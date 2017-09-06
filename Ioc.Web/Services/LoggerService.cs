namespace IoC.Web.Services
{
    public class LoggerService : ILoggerService
    {
        /// <summary>
        /// Log message
        /// </summary>
        /// <param name="message"></param>
        public void Log(string message)
        {
            Log(LogType.Info, message);
        }

        /// <summary>
        /// Log message
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        public void Log(LogType logType, string message)
        {
            // demo- no logging implementation
        }
    }
}