using WebAPIServices.Dto.Account;

namespace WebAPIServices.Services.AccountServices
{
    public interface IAccountService
    {
        Task<NewAccountDto> LoginAsync(LoginDto account);
        Task<NewAccountDto> RegisterAsync(RegisterDto account);

    }
}
