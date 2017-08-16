using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BudegetIdentityDemo.Controllers
{    
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInMgr;       

        public AuthController(SignInManager<IdentityUser> signInMgr)
        {
            _signInMgr = signInMgr;            
        }

        [HttpPost("api/auth/login")]
        public async Task<IActionResult> Login([FromBody] CredentialModel model)
        {
            var result = await _signInMgr.PasswordSignInAsync(model.UserName, model.Password, false, false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Failed to login");
        }
    }
}