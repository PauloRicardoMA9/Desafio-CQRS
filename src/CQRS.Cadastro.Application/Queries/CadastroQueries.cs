using AutoMapper;
using CQRS.Cadastro.Application.ViewModels;
using CQRS.Cadastro.Domain.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;

        public CadastroQueries(IClienteRepository clienteRepository,
                               IMapper mapper,
                               IMediatorHandler mediatorHandler)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<IList<ClienteViewModel>> ObterClientes()
        {
            var clientes = await _clienteRepository.ObterTodosClientes();

            if (clientes.Count == 0)
            {
                await _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio("ObterClientesQuerie", "Nenhum cliente cadastrado."));
                return null;
            }

            var clientesViewModel = _mapper.Map<IList<ClienteViewModel>>(clientes);
            return clientesViewModel;
        }

        public async Task<IList<ContatoViewModel>> ObterContatos()
        {
            var contatos = await _clienteRepository.ObterTodosContatos();

            if (contatos.Count == 0)
            {
                await _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio("ObterContatosQuerie", "Nenhum contato cadastrado."));
                return null;
            }

            var contatosViewModel = _mapper.Map<IList<ContatoViewModel>>(contatos);
            return contatosViewModel;
        }

        public async Task<ClienteViewModel> ObterClienteComContatoPorId(Guid clienteId)
        {
            var cliente = await _clienteRepository.ObterClientePorId(clienteId);            
            var contato = _clienteRepository.ObterContatoPorClienteId(contato => contato.ClienteId == clienteId);

            if (cliente == null)
            {
                await _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio("ObterClienteComContatoPorIdQuerie", "Nenhum cliente cadastrado com o id informado."));
                return null;
            }

            var clienteViewModel = _mapper.Map<ClienteViewModel>(cliente);

            if (contato != null)
            {
                var contatoViewModel = _mapper.Map<ContatoViewModel>(contato);
                clienteViewModel.Contato = contatoViewModel;
            }

            return clienteViewModel;
        }
    }
}
