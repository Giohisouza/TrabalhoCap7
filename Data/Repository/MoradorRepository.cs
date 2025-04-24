using Gestao.Residuos.Data.Contexts;
using Gestao.Residuos.Data.Repository;
using Gestao.Residuos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoResiduos.Data.Repository
{
    public class MoradorRepository : IMoradorRepository
    {
        private readonly DatabaseContext _context;

        public MoradorRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<MoradorModel> GetAll() => _context.Moradores.ToList();

        public IEnumerable<MoradorModel> GetAll(int page, int size)
        {
            return _context.Moradores
                           .Skip((page - 1) * size)
                           .Take(size)
                           .AsNoTracking()
                           .ToList();
        }

        public MoradorModel GetById(int id) => _context.Moradores.Find(id);

        public void Add(MoradorModel morador)
        {
            _context.Moradores.Add(morador);
            _context.SaveChanges();
        }

        public void Update(MoradorModel morador)
        {
            _context.Update(morador);
            _context.SaveChanges();
        }

        public void Delete(MoradorModel morador)
        {
            _context.Moradores.Remove(morador);
            _context.SaveChanges();
        }
    }
}
