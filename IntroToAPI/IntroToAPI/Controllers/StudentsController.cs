using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntroToAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] nameOfStudents = new string[] {"Mark", "Gregory", "Sarmiento", "Corpin"};

            //return OK represents the Success 200 in API
            return Ok(nameOfStudents);
        }
    }
}
