using CQRS.Cadastro.Domain.Interfaces;
using CQRS.Core.Data;

namespace CQRS.Cadastro.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly CadastroContext _context;

        public ClienteRepository(CadastroContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;





        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}