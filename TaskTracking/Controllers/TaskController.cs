using Microsoft.AspNetCore.Mvc;
using TaskTracking.Model;
using TaskTrackingService.Model;

namespace TaskTracking.Controllers
{
    [ApiController]
    //[Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly IDatastore _store;

        public TaskController(IDatastore store)
        {
            _store = store ?? throw new ArgumentNullException("null in class TaskController");
        }


        [HttpGet()]
        [Route("api/tasks/getallactivetasks")]
        public async Task<ActionResult<IEnumerable<MyTask>>> GetAllActiveTasks()
        {
            return Ok(await _store.GetAllActiveTasks());
        }

        [HttpGet()]
        [Route("api/tasks/getalltasks")]
        public async Task<ActionResult<IEnumerable<MyTask>>> GetAllTasks()
        {
            return Ok(await _store.GetAllTasks());
        }

        [HttpGet("{Id:Guid}")]
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
            var tt = _store.AddTask(task);

            //nedan ej färdig
            return CreatedAtRoute(routeName: nameof(GetTask),routeValues: new { },value: tt);
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