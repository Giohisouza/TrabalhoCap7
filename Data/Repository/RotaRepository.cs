using Gestao.Residuos.Data.Contexts;
using Gestao.Residuos.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Residuos.Data.Repository
{
    public class RotaRepository : IRotaRepository
    {
        private readonly DatabaseContext _context;

        public RotaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<RotaModel> GetAll() => _context.Rotas.Include(r => r.Caminhao).ToList();

        public IEnumerable<RotaModel> GetAll(int page, int size)
        {
            return _context.Rotas.Include(r => r.Caminhao)
                                 .Skip((page - 1) * size)
                                 .Take(size)
                                 .AsNoTracking()
                                 .ToList();
        }

        public RotaModel GetById(int id)
        {
            var rota = _context.Rotas
    .Include(r => r.Caminhao)
    .FirstOrDefault(r => r.Id == id);
            return rota;
        }

        public void Add(RotaModel rota)
        {
            _context.Rotas.Add(rota);
            _context.SaveChanges();
        }

        public void Update(RotaModel rota)
        {
            _context.Update(rota);
            _context.SaveChanges();
        }

        public void Delete(RotaModel rota)
        {
            _context.Rotas.Remove(rota);
            _context.SaveChanges();
        }
    }
}
