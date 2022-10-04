using Microsoft.AspNetCore.Mvc;
using TaskTracking.Model;
using TaskTrackingService.Model;

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


        [HttpGet()]
        [Route("api/tasks/getallactivetasks")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            return Ok(await _store.GetAllProjects());
        }
    }
}
