using System;

namespace Netmedia.Infrastructure.Interfaces
{
    public interface ITasksLoggerService
    {
        void RunStart();
        void RunEnd();
        void TaskStart<TaskType>(TaskType task) where TaskType : ITask;
        void TaskEnd<TaskType>(TaskType task) where TaskType : ITask;
        void WriteException<ExceptionType>(ExceptionType exception) where ExceptionType : Exception;
        void ClearLog();
        void WriteLine(string template, params string[] parameters);
        void EmptyLine();
    }
}