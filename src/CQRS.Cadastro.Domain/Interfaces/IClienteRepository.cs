using CQRS.Cadastro.Domain.Objects;
using CQRS.Core.Data;

namespace CQRS.Cadastro.Domain.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        void Adicionar(Cliente cliente);
        void Adicionar(Contato contato);
    }
}
