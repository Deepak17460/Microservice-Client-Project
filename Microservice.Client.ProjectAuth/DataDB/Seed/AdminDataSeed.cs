using Microservice.Client.ProjectAuth.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.Client.ProjectAuth.DataDB.Seed
{
    public class AdminDataSeed
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            if (!context.Roles.Any(item => item.NormalizedName == "User".Normalize()))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                await roleStore.CreateAsync(new IdentityRole() { Name = "User", NormalizedName = "User".Normalize() });
            }

            if (!context.Roles.Any(item => item.NormalizedName == "Admin".Normalize()))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                await roleStore.CreateAsync(new IdentityRole() { Name = "Admin", NormalizedName = "Admin".Normalize() });
            }

            if (!context.Users.Any(item => item.Email == "admin@gmail.com"))
            {
                var newUser = new User()
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    Name = "Admin",
                    NormalizedEmail = "admin@gmail.com".Normalize(),
                    NormalizedUserName = "admin@gmail.com".Normalize()
                };

                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(newUser, "Admin@123");
                newUser.PasswordHash = hashed;

                var userStore = new UserStore<User>(context);
                await userStore.CreateAsync(newUser);
                await AssignRoles(serviceProvider, newUser.Email, "Admin");
                context.SaveChanges();
            }
        }

        static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string roles)
        {
            UserManager<User> _userManager = services.GetService<UserManager<User>>();
            User user = await _userManager.FindByEmailAsync(email);
            await Console.Out.WriteLineAsync(user.UserName);
            var result = await _userManager.AddToRoleAsync(user, roles);

            return result;

        }
    }
}
