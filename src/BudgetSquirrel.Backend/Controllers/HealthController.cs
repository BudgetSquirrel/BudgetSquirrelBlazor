using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetSquirrel.Backend.Controllers
{
  [ApiController]
    [Route("backend/[controller]")]
    public class HealthController : Controller
    {
        [AllowAnonymous]
        [HttpGet()]
        public IActionResult GetHealth()
        {
            return Ok("healthy");
        }
    }
}
