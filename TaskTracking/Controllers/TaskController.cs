using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTrackingService.Datastore;
using TaskTrackerModels;

namespace TaskTracking.Controllers
{
    [ApiController]
    //[Authorize]
    public class TaskController : ControllerBase
    {

        private readonly IDatastore _store;

        public TaskController(IDatastore store)
        {
            _store = store ?? throw new ArgumentNullException("null in class TaskController");
        }


        [HttpGet]
        [Route("api/tasks/getallactivetasks")]
        public async Task<ActionResult<IEnumerable<MyTask>>> GetAllActiveTasks()
        {
            return Ok(await _store.GetAllActiveTasks());
        }

        [HttpGet]
        [Route("api/tasks/getalltasks")]
        public async Task<ActionResult<IEnumerable<MyTask>>> GetAllTasks()
        {
            Console.WriteLine("GetAllTasks ---->");
            return Ok(await _store.GetAllTasks());
        }

        [HttpGet()]
        [Route("api/tasks/gettask")]
        public ActionResult<MyTask> GetTask(Guid Id)
        {
            if (Id == Guid.Empty) return BadRequest();
            var t = _store.GetTask(Id);
            if (t == null) return NotFound();
            return Ok(t);
        }


        [HttpPost()]
        [Route("api/tasks/addtask")]
        public IActionResult AddTask(MyTask task)
        {
            Console.WriteLine("AddTask ---->");
            if(task is null) return BadRequest("null in argument");   
            var tt = _store.AddTask(task);
            if (!tt.Item1) return BadRequest("Failed to create a new task");
            return CreatedAtRoute(routeName: nameof(GetTask),routeValues: new { Id=task.Id},value: tt);
        }

        [HttpPut()]
        [Route("api/tasks/UpdateTask")]
        public IActionResult UpdateTask(MyTask task)
        {
            var tt = _store.UpdateTask(task);
            if(task is null || task.Id == Guid.Empty ) return BadRequest();
            if (tt.Item2 == "Invalid Id") return NotFound();
            return new NoContentResult();
        }

        /* [HttpGet(Name = "GetWeatherForecast")]
          public IEnumerable<WeatherForecast> Get()
          {
              TaskTrackingService.Test.Test y = new(_store);
              //y.TestProjectsInserts();
              //y.TestTasksInserts();
              //y.TestTaskGet();
              y.TestTaskUpdate();
              //y.TestTaskUpdateClose();
              return Enumerable.Range(1, 5).Select(index => new WeatherForecast
              {
                  Date = DateTime.Now.AddDays(index),
                  TemperatureC = Random.Shared.Next(-20, 55),
                  Summary = Summaries[Random.Shared.Next(Summaries.Length)]
              })
              .ToArray();
          }
        */


    }
}