namespace AsyncProcessing.Services;

public class TaskService : ITaskService
{
    private readonly Dictionary<Guid, Task> tasks = new();
    private readonly Dictionary<Guid, Stopwatch> taskTimes = new();

    public void AddTask(Guid guid, Task task)
    {
        tasks.Add(guid, task);
        Stopwatch stopwatch = new();
        taskTimes.Add(guid, stopwatch);
        stopwatch.Start();
    }

    public bool IsTaskNullOrCompleted(Guid guid)
    {
        var task = tasks.GetValueOrDefault(guid);
        if (task == null || task.IsCompleted)
            return true;
        else return false;
    }

    public void RemoveTask(Guid guid)
    {
        tasks.Remove(guid);
        taskTimes.Remove(guid);
    }

    public double GetTaskInvokationPercent(Guid guid, int delayCount)
    {
        var task = tasks.GetValueOrDefault(guid);
        if (task == null)
            return 100;
        else
        {
            var taskTime = taskTimes.GetValueOrDefault(guid);
            var result = (double)taskTime!.ElapsedMilliseconds / delayCount * 100;
            return result;
        }
    }
}
