using Gestao.Residuos.Data.Contexts;
using Gestao.Residuos.Data.Repository;
using Gestao.Residuos.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gestao.Residuos.Data.Repositories
{
    public class AuthUsersRepository : IAuthUsersRepository
    {
        private readonly DatabaseContext _context;

        public AuthUsersRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<AuthModel> GetByIdAsync(int id)
        {
            return await _context.Auth.FindAsync(id);
        }

        public async Task<AuthModel> GetByUserAsync(string username)
        {
            return await _context.Auth.FirstOrDefaultAsync(u => u.User == username);
        }

        public async Task<AuthModel> CreateAsync(AuthModel user)
        {
            _context.Auth.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<AuthModel> UpdateAsync(AuthModel user)
        {
            _context.Auth.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Auth.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
