using System.Threading.Tasks;
using Gestao.Residuos.Models;

namespace Gestao.Residuos.Services
{
    public interface IAuthUsersService
    {
        Task<AuthModel> AuthenticateAsync(string username, string password);
        Task<AuthModel> CreateUserAsync(string username, string password, string role);
        string GenerateJwtToken(AuthModel user);
    }
}
