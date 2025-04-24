using Gestao.Residuos.Data.Contexts;
using Gestao.Residuos.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Residuos.Data.Repository
{
    public class CaminhaoRepository : ICaminhaoRepository
    {
        private readonly DatabaseContext _context;

        public CaminhaoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<CaminhaoModel> GetAll() => _context.Caminhoes.ToList();

        public IEnumerable<CaminhaoModel> GetAll(int page, int size)
        {
            return _context.Caminhoes
                           .Skip((page - 1) * size)
                           .Take(size)
                           .AsNoTracking()
                           .ToList();
        }

        public CaminhaoModel GetById(int id) => _context.Caminhoes.Find(id);

        public void Add(CaminhaoModel caminhao)
        {
            _context.Caminhoes.Add(caminhao);
            _context.SaveChanges();
        }

        public void Update(CaminhaoModel caminhao)
        {
            _context.Update(caminhao);
            _context.SaveChanges();
        }

        public void Delete(CaminhaoModel caminhao)
        {
            _context.Caminhoes.Remove(caminhao);
            _context.SaveChanges();
        }
    }
}
