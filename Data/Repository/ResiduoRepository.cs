using Gestao.Residuos.Data.Contexts;
using Gestao.Residuos.Data.Repository;
using Gestao.Residuos.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Residuos.Data.Repository
{
    public class ResiduoRepository : IResiduoRepository
    {
        private readonly DatabaseContext _context;

        public ResiduoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<ResiduoModel> GetAll() => _context.Residuos.ToList();

        public IEnumerable<ResiduoModel> GetAll(int page, int size)
        {
            return _context.Residuos
                           .Skip((page - 1) * size)
                           .Take(size)
                           .AsNoTracking()
                           .ToList();
        }

        public ResiduoModel GetById(int id) => _context.Residuos.Find(id);

        public void Add(ResiduoModel residuo)
        {
            _context.Residuos.Add(residuo);
            _context.SaveChanges();
        }

        public void Update(ResiduoModel residuo)
        {
            _context.Update(residuo);
            _context.SaveChanges();
        }

        public void Delete(ResiduoModel residuo)
        {
            _context.Residuos.Remove(residuo);
            _context.SaveChanges();
        }
    }
}
