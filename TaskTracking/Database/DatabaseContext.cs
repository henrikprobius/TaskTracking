using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TaskTracking.Model;

namespace TaskTracking.Database
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptionsBuilder opt)
        {
            opt.UseSqlServer(@"Server=localhost\sqlexpress;Database=TaskTracker;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public DatabaseContext()
        {
           
        }

        public DbSet<MyTask>? Tasks { get; set; }
        public DbSet<Project>? Projects { get; set; }


        public async void DropAndCreatedDB()
        {
            bool deleted = await this.Database.EnsureDeletedAsync();
            Console.WriteLine($"DB deleted = {deleted}");

            bool created = await this.Database.EnsureCreatedAsync();
            Console.WriteLine($"DB created = {created}");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder opt)
        {
            opt.UseSqlServer(@"Server=localhost\sqlexpress;Database=TaskTracker;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

    }
}
