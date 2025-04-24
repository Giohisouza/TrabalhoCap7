using Gestao.Residuos.Data.Contexts;
using Gestao.Residuos.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Residuos.Data.Repository
{
    public class AgendamentoColetaRepository : IAgendamentoColetaRepository
    {
        private readonly DatabaseContext _context;

        public AgendamentoColetaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<AgendamentoColetaModel> GetAll() => _context.AgendamentosColetas.Include(c => c.Morador).ToList();

        public IEnumerable<AgendamentoColetaModel> GetAll(int page, int size)
        {
            return _context.AgendamentosColetas.Include(c => c.Morador)
                           .Skip((page - 1) * size)
                           .Take(size)
                           .AsNoTracking()
                           .ToList();
        }

        public AgendamentoColetaModel GetById(int id)
        {
            var agendamento = _context.AgendamentosColetas
            .Include(c => c.Morador)
            .FirstOrDefault(r => r.Id == id);
            return agendamento;
        }
        public void Add(AgendamentoColetaModel AgendamentoColeta)
        {
            _context.AgendamentosColetas.Add(AgendamentoColeta);
            _context.SaveChanges();
        }

        public void Update(AgendamentoColetaModel AgendamentoColeta)
        {
            _context.Update(AgendamentoColeta);
            _context.SaveChanges();
        }

        public void Delete(AgendamentoColetaModel AgendamentoColeta)
        {
            _context.AgendamentosColetas.Remove(AgendamentoColeta);
            _context.SaveChanges();
        }
    }
}
