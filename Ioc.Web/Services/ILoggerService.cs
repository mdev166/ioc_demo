namespace IoC.Web.Services
{
    public interface ILoggerService
    {
        void Log(string message);
        void Log(LogType logType, string message);
    }
}
