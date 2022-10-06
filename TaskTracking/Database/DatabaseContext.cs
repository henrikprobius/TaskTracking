using Microsoft.EntityFrameworkCore;
using TaskTrackerModels;

namespace TaskTracking.Database
{
    public class DatabaseContext:DbContext
    {

        private string connstring = @"Server=localhost\sqlexpress;Database=TaskTracker;Trusted_Connection=True;MultipleActiveResultSets=true";
        public DatabaseContext()
        {
            //opt.UseSqlServer(@"Server=localhost\sqlexpress;Database=TaskTracker;Trusted_Connection=True;MultipleActiveResultSets=true");
        }


        public DbSet<MyTask>? Tasks { get; set; }
        public DbSet<Project>? Projects { get; set; }


        public void DropAndCreateDB()
        {
            bool deleted = this.Database.EnsureDeleted();
            Console.WriteLine($"DB deleted = {deleted}");
            
            bool created = this.Database.EnsureCreated();
            Console.WriteLine($"DB created = {created}");
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder opt)
        {
            opt.UseSqlServer(connstring);
        }

      /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyTask>().Property(m => m.Status).HasColumnType("int");
            base.OnModelCreating(modelBuilder);
        }
      */

    }
}
