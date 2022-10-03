

using TaskTracking.Model;

namespace TaskTrackingService.Model
{
    public interface IDatastore
    {
        (bool,string) AddTask(MyTask task);
        (bool, string) AddProject(Project proj);
        (bool, string) UpdateTask(MyTask task);

        MyTask? GetTask(Guid guid);
        Project GetProject(Guid guid);

        List<Project> GetAllProjects();
        List<MyTask> GetAllTasks();
    }
}
