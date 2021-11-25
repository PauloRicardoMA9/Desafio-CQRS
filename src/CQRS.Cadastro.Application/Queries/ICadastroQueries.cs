using CQRS.Cadastro.Domain.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Cadastro.Application.Queries
{
    public interface ICadastroQueries
    {
        public Task<IList<Cliente>> ObterClientes();
        public Task<IList<Contato>> ObterContatos();
        public Task<Cliente> ObterCliente(Guid clienteId);
        public Task<Contato> ObterContatoPorClienteId(Guid clienteId);
        public Task<Cliente> ObterClienteComContatoPorId(Guid clienteId);
    }
}
