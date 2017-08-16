using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BudegetIdentityDemo.Controllers
{
    public class TwoFactorAuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInMgr;
        private readonly UserManager<IdentityUser> _usrMgr;
        private IPasswordHasher<IdentityUser> _hasher;

        public TwoFactorAuthController(SignInManager<IdentityUser> signInMgr, IPasswordHasher<IdentityUser> hasher, 
            UserManager<IdentityUser> usrMgr)
        {
            _signInMgr = signInMgr;
            _usrMgr = usrMgr;
            _hasher = hasher;
        }

        [HttpGet("api/twofactorauth/login")]
        public async Task<IActionResult> Login([FromBody] CredentialModel model)
        {
            var user = await _usrMgr.FindByNameAsync(model.UserName);
            if (user != null)
            {
                if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                {
                    if (user.TwoFactorEnabled && user.PhoneNumberConfirmed)
                    {
                        var code = await _usrMgr.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                        await SendSmsAsync(user.PhoneNumber, "Use OTP " + code);
                    }
                }
            }
            return BadRequest("Failed to login");
        }

        [HttpPost]                
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }            
            var result = await _signInMgr.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Failed to Login");
        }

        private Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            // Use Twilio or ClickOnce for related docs            
            return Task.FromResult(0);
        }


    }
}
