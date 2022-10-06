using TaskTrackerModels;

namespace TaskTrackingService.Datastore
{
    public interface IDatastore
    {
        (bool,string) AddTask(MyTask task);
        (bool, string) AddProject(Project proj);
        (bool, string) UpdateTask(MyTask task);

        MyTask? GetTask(Guid guid);
        Project GetProject(Guid guid);

        Task<List<Project>> GetAllProjects();

        Task<List<MyTask>> GetAllTasks();
        Task<List<MyTask>> GetAllActiveTasks();
    }
}
