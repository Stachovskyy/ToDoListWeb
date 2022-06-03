using Microsoft.AspNetCore.Identity;
using ToDoListWeb.Entities;
using ToDoListWeb.Exceptions;
using ToDoListWeb.Models;

namespace ToDoListWeb.Data
{
    public class DataBaseSeeder
    {
        public static async Task AddRole(RoleManager<Role> roleManager, string roleName)
        {
            if (roleManager.FindByNameAsync(roleName).Result == null)
            {
                Role role = new Role
                {
                    Name = roleName
                };

                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    throw new ServerErrorException($"Cannot create default role");
                }
            }
        }
        public static async Task AddAdminAsync(UserManager<User> userManager, string email, string firstName, string lastName, string password)
        {
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                User user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email
                };

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                    throw new ServerErrorException($"Cannot create default admin");
            }
        }


    }
}


