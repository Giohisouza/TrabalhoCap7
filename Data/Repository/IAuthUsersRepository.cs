using Gestao.Residuos.Models;
using System.Threading.Tasks;

namespace Gestao.Residuos.Data.Repository
{
        public interface IAuthUsersRepository
        {
            Task<AuthModel> GetByIdAsync(int id);
            Task<AuthModel> GetByUserAsync(string username);
            Task<AuthModel> CreateAsync(AuthModel user);
            Task<AuthModel> UpdateAsync(AuthModel user);
            Task DeleteAsync(int id);
        }
    }
