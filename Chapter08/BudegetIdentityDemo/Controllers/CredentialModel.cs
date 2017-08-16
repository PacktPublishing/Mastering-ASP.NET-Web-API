using System.ComponentModel.DataAnnotations;

namespace BudegetIdentityDemo.Controllers
{
    public class CredentialModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}