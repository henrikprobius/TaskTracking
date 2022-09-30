

using TaskTracking.Model;

namespace TaskTrackingService.Model
{
    public interface IDatastore
    {
        (bool,string) AddTask(MyTask task);
        (bool, string) UpdateTask(MyTask task);

        MyTask? GetTask(Guid guid);
    }
}
