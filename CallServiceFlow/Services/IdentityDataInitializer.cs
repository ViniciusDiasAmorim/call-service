using CallServiceFlow.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CallServiceFlow.Services
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);

            await SeedAdminUser(userManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Tecnico", "Cliente" };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);

                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                    Console.WriteLine($"Role {roleName} criada com sucesso.");
                }
            }
        }

        private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
        {
            var adminUser = new ApplicationUser
            {
                UserName = "admin@exemplo.com",
                Email = "admin@exemplo.com",
                EmailConfirmed = true,
                Name = "Administrador",
                CreationDate = DateTime.Now,
            };

            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                var result = await userManager.CreateAsync(adminUser, "Admin@123456");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    await userManager.AddClaimAsync(adminUser, new Claim("FullAccess", "true"));

                    Console.WriteLine("Usuário administrador criado com sucesso.");
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    Console.WriteLine($"Erro ao criar usuário administrador: {errors}");
                }
            }
        }
    }
}
