namespace AsyncProcessing.Services
{
    public interface ITaskService
    {
        public bool IsTaskNullOrCompleted(Guid guid);
        public void RemoveTask(Guid guid);
        public void AddTask(Guid guid, Task task);
        public double GetTaskInvokationPercent(Guid guid, int delayCount);
    }
}