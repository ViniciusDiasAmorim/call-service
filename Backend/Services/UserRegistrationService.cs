using CallServiceFlow.Model;
using CallServiceFlow.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CallServiceFlow.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRegistrationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool ok, string message)> Register(Register model, string role)
        {
            var validations = ValidateRegisterModel(model);

            if (!validations.ok)
                return (false, validations.message);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                CreationDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return (false, result.Errors.FirstOrDefault().Description);

            await _userManager.AddToRoleAsync(user, role);

            return (true, "Usuário registrado com sucesso!");
        }

        private (bool ok, string message) ValidateRegisterModel(Register model)
        {
            if (string.IsNullOrEmpty(model.Email))
                return (false, "Email não pode ser vazio");

            if (string.IsNullOrEmpty(model.Password))
                return (false, "Senha não pode ser vazia");

            if (model.Password.Length < 8)
                return (false, "A senha não pode ter menos que 8 caracteres");

            if (model.Password != model.ConfirmPassword)
                return (false, "As senhas não são iguais");


            return (true, null);
        }
    }
}
