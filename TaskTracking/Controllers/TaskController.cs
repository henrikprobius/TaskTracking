using Microsoft.AspNetCore.Mvc;
using TaskTracking.Model;
using TaskTrackingService.Model;

namespace TaskTracking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly IDatastore _store;

        public TaskController(IDatastore store)
        {
            _store = store;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            TaskTrackingService.Test.Test y = new(_store);
            //y.TestProjectsInserts();
            //y.TestTasksInserts();
            y.TestTaskGet();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }



    }
}