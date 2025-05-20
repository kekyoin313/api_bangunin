using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praktimupaa2.Models.Person;


namespace praktimupaa2.Controllers
{
    [Authorize]
    [Route("bangunin/v1/[controller]")]
    public class PersonController : ControllerBase
    {
        private string _consStr;
       
        public PersonController(
            IConfiguration configuration) {
            _consStr = configuration.GetConnectionString("DefaultConnection");
        }
        [HttpGet,Authorize]
        public  ActionResult<Person> getPersonWithAuth()
        {
            PersonContext context = new PersonContext(_consStr);
            List<Person> result = context.getPersonWithAuth(); 
            return Ok(result);
        }



    }
}
