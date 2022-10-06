using Microsoft.AspNetCore.Mvc;
using TaskTrackingService.Datastore;
using TaskTrackerModels;


namespace TaskTrackingService.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IDatastore _store;

        public ProjectController(IDatastore store)
        {
            _store = store ?? throw new ArgumentNullException("null in class ProjectController");
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            Console.WriteLine("GetAllProjects --->");
            return Ok(await _store.GetAllProjects());
        }
    }
}
