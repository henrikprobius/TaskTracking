

using TaskTracking.Model;

namespace TaskTrackingService.Test
{
    public class Test
    {
        private TaskTrackingService.Model.IDatastore _store;
        public Test(TaskTrackingService.Model.IDatastore store)
        { _store = store ?? throw new ArgumentNullException("null in Test-class"); }

        public void TestTasksInserts()
        {
            Console.WriteLine();
            Console.WriteLine("------- TestTasks Inserts --->");
            var projs = _store.GetAllProjects();
            (bool success, string msg) ret;
            MyTask t = new MyTask("Title1", "Descr1", DateTime.Now.AddDays(60));
            t.Project = projs[0];
            
            ret = _store.AddTask(t);
            Console.WriteLine($"Adding new Task. Success = {ret.success}, Message = {ret.msg}");


            t = new MyTask("Title2", "Descr2", DateTime.Now.AddDays(90));
            t.Project = projs[1];
            ret = _store.AddTask(t);
            Console.WriteLine($"Adding new Task. Success = {ret.success}, Message = {ret.msg}");

            t = new MyTask("Title3", "Descr3", DateTime.Now.AddDays(80));
            ret = _store.AddTask(t);
            Console.WriteLine($"Adding new Task. Success = {ret.success}, Message = {ret.msg}");
            Console.WriteLine();
        }

        public void TestProjectsInserts()
        {
            Console.WriteLine();
            Console.WriteLine("------- TestProject Inserts --->");
            (bool success, string msg) ret;
            Project t = new Project("Project1");

            ret = _store.AddProject(t);
            Console.WriteLine($"Adding new Project. Success = {ret.success}, Message = {ret.msg}");

            t = new Project("Project2");
            ret = _store.AddProject(t);
            Console.WriteLine($"Adding new Project. Success = {ret.success}, Message = {ret.msg}");

            t = new Project("Project3");
            ret = _store.AddProject(t);
            Console.WriteLine($"Adding new Project. Success = {ret.success}, Message = {ret.msg}");
            Console.WriteLine();
        }

        public void TestTaskGet()
        {
            Console.WriteLine();
            Console.WriteLine("------- Test getting tasks --->");
            foreach (var item in _store.GetAllTasks())
            {
                if (item.Project is null )
                    Console.WriteLine($"TaskName: {item.Title} has no project and status {item.Status}");
                else
                    Console.WriteLine($"TaskName: {item.Title} has project {item.Project.Name} and status {item.Status}");
            }
            Console.WriteLine();

        }
    }
}
