namespace Netmedia.Infrastructure.Interfaces
{
    public interface ITasksRunnerService
    {
        void RunAllTasks(string[] startupArgs);
    }
}