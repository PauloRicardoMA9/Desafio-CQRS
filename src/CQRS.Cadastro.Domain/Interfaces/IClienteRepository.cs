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
        public Task<IList<Cliente>> BuscarCliente(Expression<Func<Cliente, bool>> predicate);
        public Task<IList<Contato>> BuscarContato(Expression<Func<Contato, bool>> predicate);
        public Task<IList<Cliente>> ObterTodosClientes();
        public Task<IList<Contato>> ObterTodosContatos();
        public Task<Cliente> ObterClientePorId(Guid id);
        public Task<Contato> ObterContatoPorId(Guid id);
        public Contato ObterContatoPorClienteId(Expression<Func<Contato, bool>> predicate);
        public void RemoverCliente(Guid id);
        public void RemoverContato(Guid id);
        public void AtualizarCliente(Cliente cliente);
        public void AtualizarContato(Contato contato);
    }
}
