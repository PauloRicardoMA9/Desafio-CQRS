using CQRS.Cadastro.Domain.Interfaces;
using CQRS.Cadastro.Domain.Objects;
using CQRS.Core.Communication.Mediator;
using CQRS.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRS.Cadastro.Application.Queries
{
    public class CadastroQueries : ICadastroQueries
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public CadastroQueries(IClienteRepository clienteRepository,
                               IMediatorHandler mediatorHandler)
        {
            _clienteRepository = clienteRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<IList<Cliente>> ObterClientes()
        {
            var clientes = await _clienteRepository.ObterTodosClientes();

            if (clientes.Count == 0)
            {
                await _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio("ObterClientesQuerie", "Nenhum cliente cadastrado."));
                return null;
            }

            return clientes;
        }

        public async Task<IList<Contato>> ObterContatos()
        {
            var contatos = await _clienteRepository.ObterTodosContatos();

            if (contatos.Count == 0)
            {
                await _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio("ObterContatosQuerie", "Nenhum contato cadastrado."));
                return null;
            }

            return contatos;
        }

        public async Task<Cliente> ObterCliente(Guid clienteId)
        {
            var cliente = await _clienteRepository.ObterClientePorId(clienteId);

            if (cliente == null)
            {
                await _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio("ObterClienteQuerie", "Nenhum cliente cadastrado com o id informado."));
                return null;
            }

            return cliente;
        }

        public async Task<Contato> ObterContatoPorClienteId(Guid clienteId)
        {
            var contato = _clienteRepository.ObterContatoPorClienteId(contato => contato.ClienteId == clienteId);

            if (contato == null)
            {
                await _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio("ObterContatoPorClienteIdQuerie", "Nenhum contato cadastrado cujo cliente possua o id informado."));
                return null;
            }

            return contato;
        }

        public async Task<Cliente> ObterClienteComContatoPorId(Guid clienteId)
        {
            var cliente = await _clienteRepository.ObterClientePorId(clienteId);            
            var contato = _clienteRepository.ObterContatoPorClienteId(contato => contato.ClienteId == clienteId);

            if (cliente == null)
            {
                await _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio("ObterClienteQuerie", "Nenhum cliente cadastrado com o id informado."));
                return null;
            }

            if (contato != null)
            {
                cliente.AlterarContato(contato);
            }

            return cliente;
        }
    }
}
