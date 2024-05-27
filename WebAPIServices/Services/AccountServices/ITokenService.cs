using WebAPIServices.Models;

namespace WebAPIServices.Services.AccountServices
{
    public interface ITokenService
    {
        string CreateToken(Account account, string userRole);
    }
}
