using CallServiceFlow.Dto.CustomerDTO;

namespace CallServiceFlow.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool ok, string message, CustomerResponseDto responseDto)> CreateCustomer(CustomerDto customerDto);
    }
}