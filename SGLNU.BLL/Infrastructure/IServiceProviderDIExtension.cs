using SGLNU.BLL.Configs;
using SGLNU.DAL.Enteties;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SGLNU.BLL.Infrastructure
{
    public static class IServiceProviderDIExtension
    {
        public static async Task CreateUserRoles(this IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in new string[] { "ActiveMember", "FacultyMember", "FacultySecretary", "AlternateFacultyAdmin", "FacultyAdmin", "Secretary", "AlternateAdmin", "Admin" })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var adminConfig = serviceProvider.GetRequiredService<IOptions<AdminConfig>>();

            if (await userManager.FindByEmailAsync(adminConfig.Value.Email) == null)
            {
                var admin = new AppUser
                {
                    UserName = adminConfig.Value.Email,
                    Email = adminConfig.Value.Email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, adminConfig.Value.Password);
                await userManager.AddToRolesAsync(admin, new string[] { "ActiveMember", "FacultyMember", "FacultySecretary", "AlternateFacultyAdmin", "FacultyAdmin", "Secretary", "AlternateAdmin", "Admin" });
            }
        }
    }
}
