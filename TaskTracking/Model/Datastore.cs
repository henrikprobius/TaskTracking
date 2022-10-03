using Microsoft.EntityFrameworkCore;
using System;
using TaskTracking.Database;
using TaskTracking.Model;

namespace TaskTrackingService.Model
{
    public class Datastore: IDatastore
    {

        private TaskTracking.Database.DatabaseContext _context;

        public Datastore(TaskTracking.Database.DatabaseContext context)
        {
            Console.WriteLine("Constructor injection in Datastore class");
            _context = context ?? throw new ArgumentNullException("null in Datastore contructor"); 
        }


        public (bool,string) AddTask(MyTask task)
        {            
            task.Created = DateTime.Now;
            task.Id = Guid.NewGuid();
            if (task.Created > task.DueDate) return (false, "date created cannot be larger than duedate");

            _context.Tasks.Add(task);
            if(_context.SaveChanges() < 1) return (false, "could not save created Task into DB");
            
            return (true, "OK");
         }

        public (bool, string) AddProject(Project proj)
        {
            proj.Id = Guid.NewGuid();
            _context.Projects.Add(proj);
            if (_context.SaveChanges() < 1) return (false, "could not save created Project into DB");

            return (true, "OK");
        }

        public List<Project> GetAllProjects()
        {
            return _context.Projects.ToList();  
        }

        public List<MyTask> GetAllTasks()
        {
            //db.Tasks.Include(o => o.Person).Single(o => o.person_id == personId);
            return _context.Tasks.Include(o => o.Project).ToList();
        }

        public (bool, string) UpdateTask(MyTask task)
        {
            var t = _context.Tasks.FirstOrDefault(c => c.Id == task.Id);
            if (t != null) return (false, "Id is not in DB");
            t = t + task;  //update db object with values from parameter
            if (_context.SaveChanges() < 1) return (false, "could not save an updated Task in DB");

            return (true, "OK");
        }

        public MyTask GetTask(Guid guid)
        {
            return _context.Tasks.FirstOrDefault(c => c.Id == guid);
        }

        public Project GetProject(Guid guid)
        {
            return _context.Projects.FirstOrDefault(c => c.Id == guid);
        }


        /********** Test Area ***********/
        public void TestInserts()
        {
            Console.WriteLine();
            Console.WriteLine("******* Testing inserts *******");
            (bool success, string msg) ret;
            MyTask t = new MyTask("Title1", "Descr1", DateTime.Now.AddDays(60));

            ret = AddTask(t);
            Console.WriteLine($"Adding new Task. Success = {ret.success}, Message = {ret.msg}");


            t = new MyTask("Title2", "Descr2", DateTime.Now.AddDays(90));
            ret = AddTask(t);
            Console.WriteLine($"Adding new Task. Success = {ret.success}, Message = {ret.msg}");

            t = new MyTask("Title3", "Descr2", DateTime.Now.AddDays(80));
            ret = AddTask(t);
            Console.WriteLine($"Adding new Task. Success = {ret.success}, Message = {ret.msg}");
            Console.WriteLine();
        }
    }
}

