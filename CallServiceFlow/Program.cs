using CallServiceFlow.Context;
using CallServiceFlow.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BCrypt.Net;

namespace CallServiceFlow
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            // Configura��o do Identity com bcrypt
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Configura��es de senha
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                // Configura��es de usu�rio
                options.User.RequireUniqueEmail = true;
                // Configura��es de bloqueio
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Registra o hasher personalizado
            builder.Services.AddScoped<IPasswordHasher<ApplicationUser>, BCryptPasswordHasher<ApplicationUser>>();

            // Registra o servi�o JWT
            builder.Services.AddScoped<JwtService>();

            // Configura��o do JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
                };
            });

            // Configura��o de autoriza��o baseada em roles
            builder.Services.AddAuthorization(options =>
            {
                // Pol�tica para Admin
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                // Pol�tica para T�cnicos
                options.AddPolicy("TecnicoAccess", policy => policy.RequireRole("Admin", "Tecnico"));
                // Pol�tica para Clientes
                options.AddPolicy("ClienteAccess", policy => policy.RequireRole("Admin", "Cliente"));
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Adiciona o middleware de autentica��o ANTES da autoriza��o
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // Inicializa as roles e o usu�rio admin
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();

                try
                {
                    context.Database.OpenConnection();
                    Console.WriteLine("Conex�o bem-sucedida!");
                    context.Database.CloseConnection();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao conectar ao banco: {ex.Message}");
                }

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await IdentityDataInitializer.SeedData(userManager, roleManager);

                //var services = scope.ServiceProvider;
                //var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                //// Chama o m�todo de seed
                //await IdentityDataInitializer.SeedData(userManager, roleManager);
            }

            app.Run();
        }
    }
}