using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BudegetIdentityDemo
{
    public class IdentityDbSeeder
    {
        private RoleManager<IdentityRole> _roleMgr;
        private UserManager<IdentityUser> _userMgr;
        
        public IdentityDbSeeder(UserManager<IdentityUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
        }

        public async Task Seed()
        {
            var user = await _userMgr.FindByNameAsync("mithunvp");

            // Add User
            if (user == null)
            {
                if (!(await _roleMgr.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole("Admin");
                    await _roleMgr.AddClaimAsync(role, new Claim("IsAdmin", "True"));
                    await _roleMgr.CreateAsync(role);
                }

                user = new IdentityUser()
                {
                    UserName = "mithunvp",                    
                    Email = "mithunvp@packt.com",
                };

                var userResult = await _userMgr.CreateAsync(user, "P@ssw0rd!123");
                var roleResult = await _userMgr.AddToRoleAsync(user, "Admin");
                var claimResult = await _userMgr.AddClaimAsync(user, new Claim("SuperUser", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Build user and roles failed");
                }

            }
        }
    }
}
