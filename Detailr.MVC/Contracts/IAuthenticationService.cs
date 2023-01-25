using Detailr.MVC.Models;

namespace Detailr.MVC.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(RegisterVM register);
        Task Logout();
    }
}
