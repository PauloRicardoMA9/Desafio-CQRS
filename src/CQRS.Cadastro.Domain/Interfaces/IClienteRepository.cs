using CQRS.Cadastro.Domain.Objects;
using CQRS.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRS.Cadastro.Domain.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        void Adicionar(Cliente cliente);
        void Adicionar(Contato contato);
        public Task<IEnumerable<Cliente>> BuscarCliente(Expression<Func<Cliente, bool>> predicate);
        public Task<IEnumerable<Contato>> BuscarContato(Expression<Func<Contato, bool>> predicate);
        public void RemoverCliente(Guid id);
        public void RemoverContato(Guid id);
    }
}
