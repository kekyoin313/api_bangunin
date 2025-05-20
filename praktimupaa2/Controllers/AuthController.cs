using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using praktimupaa2.Helpers;
using praktimupaa2.Models.Auth;
using praktimupaa2.Models.Login;
using praktimupaa2.Models.Person;

namespace praktimupaa2.Controllers
{
    [AllowAnonymous]
    [Route("bangunin/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string __constr;
        private readonly IConfiguration _config;
       
        public AuthController(IConfiguration config)
        {
            _config = config;
            __constr = _config.GetConnectionString("DefaultConnection");
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] Person registerData)
        {

            
            AuthContext context = new AuthContext(__constr);


            
            bool isRegistered = context.RegisterPerson(registerData);

            if (isRegistered)
            {
                return Ok(new { message = "Registrasi berhasil "});
            }
            else
            {
                return StatusCode(500, new { message = "Registrasi gagal" });
            }
        }



        [HttpPost("login")]
        public IActionResult Login([FromBody] Auth loginData)
        {
            AuthContext context = new AuthContext(__constr);
            Person person = context.Auth(loginData.name.ToString(), loginData.password.ToString());

            if (person == null )
            {
                return Unauthorized(new { message = "Nama atau password salah"});
            }
          
            JwtHelper helper = new JwtHelper(_config);
            string token = helper.GenerateJwtToken(loginData);


            return Ok(new
            {
                token = token,
                person = new
                {
                    id = person.id_person,
                    nama = person.nama
                }
            });
        }

    }
}
