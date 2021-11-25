using CQRS.Cadastro.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Cadastro.Application.Queries
{
    public interface ICadastroQueries
    {
        public Task<IList<ClienteViewModel>> ObterClientes();
        public Task<IList<ContatoViewModel>> ObterContatos();
        public Task<ClienteViewModel> ObterClienteComContatoPorId(Guid clienteId);
    }
}
