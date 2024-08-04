using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendRestAPI.Controllers
{
    public class ErrorsController : ApiController
    {
        [HttpGet]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
