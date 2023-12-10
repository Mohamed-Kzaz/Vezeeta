using Vezeeta.API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gym.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    // When The Project Running, It Will Not Appear In Swagger
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        //when Frontend or Mobile developer consume on endpoint 
        //not find in my project will redirect for another endpoint I craeted.
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code));
        }

    }
}
