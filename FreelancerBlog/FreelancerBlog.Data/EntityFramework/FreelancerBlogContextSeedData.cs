using System;
using System.Linq;
using FreelancerBlog.Core.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FreelancerBlog.Data.EntityFramework
{
    public class FreelancerBlogContextSeedData
    {
        public async void SeedAdminUser()
        {
            var user = new ApplicationUser
            {
                UserName = "Xellarix@gmail.com",
                NormalizedUserName = "xellarix@gmail.com",
                Email = "Xellarix@gmail.com",
                NormalizedEmail = "xellarix@gmail.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserRegisteredDate = DateTime.Now,
                UserFullName = "Hamid Mosalla",
                UserGender = "Male"
            };

            using (var _context = new FreelancerBlogContext())
            {
                var roleStore = new RoleStore<IdentityRole>(_context);

                if (!_context.Roles.Any(r => r.Name == "admin"))
                {
                    await roleStore.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "admin" });
                }

                if (!_context.Users.Any(u => u.UserName == user.UserName))
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var hashed = password.HashPassword(user, "Mc2^6csQ^U88H5pz");
                    user.PasswordHash = hashed;
                    var userStore = new UserStore<ApplicationUser>(_context);
                    await userStore.CreateAsync(user);
                    await userStore.AddToRoleAsync(user, "admin");
                }

                await _context.SaveChangesAsync(); 
            }
        }
    }
}