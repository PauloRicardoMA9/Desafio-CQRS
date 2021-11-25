using CQRS.Cadastro.Domain.Interfaces;
using CQRS.Cadastro.Domain.Objects;
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

        public void Adicionar(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
        }

        public void Adicionar(Contato contato)
        {
            _context.Contatos.Add(contato);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}