using CQRS.Cadastro.Domain.Interfaces;
using CQRS.Cadastro.Domain.Objects;
using CQRS.Core.Communication.Mediator;
using CQRS.Core.Messages;
using CQRS.Core.Messages.CommonMessages.Notifications;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Cadastro.Application.Commands
{
    public class CadastroCommandHandler : IRequestHandler<ComandoAdicionarCliente, bool>,
                                          IRequestHandler<ComandoAdicionarContato, bool>,
                                          IRequestHandler<ComandoRemoverCliente, bool>,
                                          IRequestHandler<ComandoRemoverContato, bool>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public CadastroCommandHandler(IClienteRepository clienteRepository, IMediatorHandler mediatorHandler)
        {
            _clienteRepository = clienteRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(ComandoAdicionarCliente comando, CancellationToken cancellationToken)
        {
            if (!ValidarComando(comando))
            {
                return false;
            }

            var cliente = new Cliente(comando.ClienteId, comando.Nome, comando.Sobrenome, comando.Cpf, (Sexo)comando.Sexo, null);
            _clienteRepository.Adicionar(cliente);

            return await _clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ComandoAdicionarContato comando, CancellationToken cancellationToken)
        {
            if (!ValidarComando(comando))
            {
                return false;
            }

            var clienteCadastrado = _clienteRepository.BuscarCliente(cliente => cliente.Id == comando.ClienteId).Result.Any();

            if (!clienteCadastrado)
            {
                await _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio(comando.TipoDaMensagem, "Nenhum cliente cadastrado com o id informado."));
                return false;
            }

            var contato = new Contato(comando.ContatoId, comando.ClienteId, comando.Ddd, comando.Telefone, comando.Email);
            _clienteRepository.Adicionar(contato);

            return await _clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ComandoRemoverCliente comando, CancellationToken cancellationToken)
        {
            if (!ValidarComando(comando))
            {
                return false;
            }

            var clienteCadastrado = _clienteRepository.BuscarCliente(cliente => cliente.Id == comando.ClienteId).Result.Any();

            if (!clienteCadastrado)
            {
                await _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio(comando.TipoDaMensagem, "Nenhum cliente cadastrado com o id informado."));
                return false;
            }

            _clienteRepository.RemoverCliente(comando.ClienteId);

            return await _clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ComandoRemoverContato comando, CancellationToken cancellationToken)
        {
            if (!ValidarComando(comando))
            {
                return false;
            }

            var contatoCadastrado = _clienteRepository.BuscarContato(contato => contato.Id == comando.ContatoId).Result.Any();

            if (!contatoCadastrado)
            {
                await _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio(comando.TipoDaMensagem, "Nenhum contato cadastrado com o id informado."));
                return false;
            }

            _clienteRepository.RemoverContato(comando.ContatoId);

            return await _clienteRepository.UnitOfWork.Commit();
        }

        private bool ValidarComando(Comando comando)
        {
            if (comando.EhValido())
            {
                return true;
            }

            foreach (var erro in comando.ResultadoDaValidacao.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio(comando.TipoDaMensagem, erro.ErrorMessage));
            }

            return false;
        }
    }
}
