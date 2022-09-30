using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracking.Model
{
    public class MyTask
    {
        public MyTask(String title,String description, DateTime duedate)
        {
            this.Title = title;
            this.Description = description; 
            this.DueDate = duedate; 
        }

        private TaskStatus status = TaskStatus.Active;

        public MyTask() { }

        [Key]
        public Guid Id { get; set; } = Guid.Empty;

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        //will be nvarchar(max) be default ion SQL
        public string Description { get; set; }

        [Required]
        public DateTime? Created { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public TaskStatus Status {

            get
            {
                if ((status != TaskStatus.Closed) && (DateTime.Now > DueDate))
                    return TaskStatus.Due;
                return status;
            }
            set
            {
                status = value;
            }
        }


        public Project? Project { get; set; }


        //oparator overloading that maps data from fields
        public static MyTask operator + (MyTask task1, MyTask task2)
        {
            task1.Status = task2.Status;
            task1.Title = task2.Title;  
            task1.Description = task2.Description;  
            task1.DueDate = task2.DueDate;
            task1.Project = task2.Project;
            return task1;
        }
    }// class Task



    public class Project
    {

        public Project(string name) { Name = name; }

        [Key]
        public Guid Id { get; set; } = Guid.Empty;

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public Project() { }
    }


    [System.Flags]
    public enum TaskStatus : uint  //32 Statuses are the maximum
    {
        Active = 0,
        Due = 2,
        Closed = 4,


    }// enum Brand
}
