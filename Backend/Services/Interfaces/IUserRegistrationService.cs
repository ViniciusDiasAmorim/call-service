using CallServiceFlow.Model;

namespace CallServiceFlow.Services.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<(bool ok, string message)> Register(Register model, string role);
    }
}