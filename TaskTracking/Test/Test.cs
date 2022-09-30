

using TaskTracking.Model;

namespace TaskTrackingService.Test
{
    public class Test
    {
        private TaskTrackingService.Model.Datastore _store;
        public Test(TaskTrackingService.Model.Datastore store)
        { _store = store ?? throw new ArgumentNullException("null in Test-class"); }

        public void TestInserts()
        {
            (bool success, string msg) ret;
            MyTask t = new MyTask("Title1","Descr1",DateTime.Now.AddDays(60));

            ret = _store.AddTask(t);
            Console.WriteLine($"Adding new Task. Success = {ret.success}, Message = {ret.msg}");


            t = new MyTask("Title2", "Descr2", DateTime.Now.AddDays(90));
            ret = _store.AddTask(t);
            Console.WriteLine($"Adding new Task. Success = {ret.success}, Message = {ret.msg}");

            t = new MyTask("Title3", "Descr2", DateTime.Now.AddDays(80));
            ret = _store.AddTask(t);
            Console.WriteLine($"Adding new Task. Success = {ret.success}, Message = {ret.msg}");
        }
    }
}
