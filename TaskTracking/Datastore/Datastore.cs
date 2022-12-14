using Microsoft.EntityFrameworkCore;
using TaskTrackerModels;

namespace TaskTrackingService.Datastore
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
            Console.WriteLine("Datastore.AddTask");
            task.Created = DateTime.Now;
            task.Id = Guid.NewGuid();
            if (task.Created > task.DueDate) return (false, "date created cannot be larger than DueDate");

            //_context.Entry(task).State = EntityState.Detached;
            _context.Tasks.Add(task);
            _context.ChangeTracker.Entries<Project>().ToList().ForEach(p => p.State = EntityState.Unchanged);   
                
            if (_context.SaveChanges() < 1) return (false, "could not save created Task into DB");
            
            return (true, "OK");
         }

        public (bool, string) AddProject(Project proj)
        {
            proj.Id = Guid.NewGuid();
            _context.Projects.Add(proj);
            if (_context.SaveChanges() < 1) return (false, "could not save created Project into DB");

            return (true, "OK");
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return await _context.Projects.OrderBy(c => c.Name).ToListAsync();  
        }

        public async Task<List<MyTask>> GetAllTasks()
        {
            return await _context.Tasks.Include(o => o.Project).OrderBy(o => o.DueDate).ToListAsync();
        }

        public async Task<List<MyTask>> GetAllActiveTasks()
        {

            return await _context.Tasks.Include(o => o.Project).Where(o =>o.Status != TaskTrackerModels.TaskStatus.Closed).OrderBy(o => o.DueDate).ToListAsync();

        }

        public (bool, string) UpdateTask(MyTask task)
        {         
            var t = this.GetTask(task.Id);
            if (t == null) return (false, "Invalid Id");
            t.Status = task.Status;
            t.DueDate = task.DueDate;
            t.Title = task.Title;
            t.Description = task.Description;
            
            //t = t + task;  //update db object with values from parameter
            //_context.ChangeTracker.Entries<Project>().ToList().ForEach(p => p.State = EntityState.Unchanged);
            //'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.'

            if (_context.SaveChanges() < 1) return (false, "could not save an updated Task in DB");

            return (true, "OK");

        }

        public MyTask GetTask(Guid guid)
        {
            return _context.Tasks.Include(o => o.Project).FirstOrDefault(c => c.Id == guid);
     
        }

        public Project GetProject(Guid guid)
        {
            return _context.Projects.FirstOrDefault(c => c.Id == guid);
        }

    }
}

