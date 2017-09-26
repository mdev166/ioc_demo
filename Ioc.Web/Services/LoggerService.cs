namespace IoC.Web.Services
{
    public class LoggerService : ILoggerService
    {
        #region ILoggerService methods

        public void Log(string message)
        {
            Log(LogType.Info, message);
        }

        public void Log(LogType logType, string message)
        {
            // demo- not implemented
        }

        #endregion
    }
}