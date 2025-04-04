using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CallServiceFlow.Services
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Cria roles se não existirem
            await SeedRoles(roleManager);

            // Cria usuário admin se não existir
            await SeedAdminUser(userManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Tecnico", "Cliente" };

            foreach (var roleName in roleNames)
            {
                // Verifica se a role já existe
                var roleExists = await roleManager.RoleExistsAsync(roleName);

                if (!roleExists)
                {
                    // Cria a role se não existir
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                    Console.WriteLine($"Role {roleName} criada com sucesso.");
                }
            }
        }

        private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager)
        {
            // Dados do usuário admin padrão
            var adminUser = new ApplicationUser
            {
                UserName = "admin@exemplo.com",
                Email = "admin@exemplo.com",
                EmailConfirmed = true,
                Nome = "Administrador",
                DataCriacao = DateTime.Now,
                RefreshToken = ""
            };

            // Verifica se o usuário admin já existe
            var user = await userManager.FindByEmailAsync(adminUser.Email);

            if (user == null)
            {
                // Cria o usuário admin com senha
                var result = await userManager.CreateAsync(adminUser, "Admin@123456");

                if (result.Succeeded)
                {
                    // Atribui a role Admin ao usuário
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    // Adiciona claims para o usuário admin
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
