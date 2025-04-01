using CallServiceFlow.Model;
using CallServiceFlow.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace CallServiceFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtService _jwtService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // Valida o modelo
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Cria o usuário
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Nome = model.Nome,
                DataCriacao = DateTime.Now
            };

            // Tenta criar o usuário
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Atribui a role Cliente por padrão
            await _userManager.AddToRoleAsync(user, "Cliente");

            return Ok(new { message = "Usuário registrado com sucesso!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            // Valida o modelo
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Encontra o usuário pelo email
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized(new { message = "Email ou senha inválidos." });

            // Verifica a senha
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new { message = "Email ou senha inválidos." });

            // Atualiza o último acesso
            user.UltimoAcesso = DateTime.Now;
            await _userManager.UpdateAsync(user);

            // Gera o token JWT
            var tokenResponse = await _jwtService.GenerateTokenAsync(user);

            return Ok(tokenResponse);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            try
            {
                var tokenResponse = await _jwtService.RefreshTokenAsync(model.Token, model.RefreshToken);
                return Ok(tokenResponse);
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null)
                return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                user.Id,
                user.Email,
                user.Nome,
                user.DataCriacao,
                user.UltimoAcesso,
                Roles = roles
            });
        }
    }
}
