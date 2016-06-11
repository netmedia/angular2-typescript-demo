namespace Netmedia.Infrastructure.Interfaces
{
    public interface IConditionalTask : ITask
    {
        bool ShouldBeExecuted(string[] args);
    }
}