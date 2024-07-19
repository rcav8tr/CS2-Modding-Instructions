using Colossal.Logging;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace ReplaceThisNamespaceWithYourOwn
{
    /// <summary>
    /// Utility routines for logging.
    /// </summary>
    public static class LogUtil
    {
        // Create a new log just for this mod.
        // This mod will have its own log file in the game's Logs folder.
        private static readonly ILog _log = LogManager.GetLogger(ModAssemblyInfo.Name);

        /// <summary>
        /// Log an info message.
        /// </summary>
        public static void Info(string message)
        {
            _log.Info(message);

            // Info messages are not written to the BepInEx console by the Colossal logger, so write the message explicitly.
            // Include the mod assembly name and message level to make info messages appear similar to other messages.
            Console.WriteLine($"[{ModAssemblyInfo.Name}] [INFO]  {message}");
        }

        /// <summary>
        /// Log a warning message.
        /// </summary>
        public static void Warn(string message)
        {
            _log.Warn(message);
        }

        /// <summary>
        /// Log an error message.
        /// </summary>
        public static void Error(string message)
        {
            _log.Error(message);
        }

        /// <summary>
        /// Log a critical message.
        /// </summary>
        public static void Critical(string message)
        {
            _log.Critical(message);
        }

        /// <summary>
        /// Log an exception with stack trace.
        /// </summary>
        public static void Exception(Exception ex)
        {
            // Build stack trace from the frames.
            // Start at index 1 to skip the call to LogUtil.Exception.
            StringBuilder stackTrace = new StringBuilder();
            StackFrame[] stackFrames = (new StackTrace()).GetFrames();
            for (int i = 1; i < stackFrames.Length; i++)
            {
                // Build a parameter list for the method.
                StringBuilder parameterList = new StringBuilder();
                MethodBase stackFrameMethod = stackFrames[i].GetMethod();
                ParameterInfo[] parameters = stackFrameMethod.GetParameters();
                foreach (ParameterInfo param in parameters)
                {
                    parameterList.Append((parameterList.Length == 0 ? "" : ", ") + param.ParameterType.GetTypeInfo().Name);
                }
                
                // Append the method with its parameter list.
                stackTrace.Append(Environment.NewLine + $"  at {stackFrameMethod.ReflectedType}.{stackFrameMethod.Name}({parameterList})");
            }

            // Log the exception as critical.
            _log.Critical(ex.GetType() + ": " + ex.Message + stackTrace);
        }
    }
}
