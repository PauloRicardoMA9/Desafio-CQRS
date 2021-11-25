using CQRS.Cadastro.Application.Queries;
using CQRS.Cadastro.Domain.Interfaces;
using CQRS.Cadastro.Domain.Objects;
using CQRS.Core.Communication.Mediator;
using CQRS.Core.Messages;
using CQRS.Core.Messages.CommonMessages.Notifications;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Cadastro.Application.Commands
{
    public class CadastroCommandHandler : IRequestHandler<ComandoAdicionarCliente, bool>,
                                          IRequestHandler<ComandoAdicionarContato, bool>,
                                          IRequestHandler<ComandoRemoverCliente, bool>,
                                          IRequestHandler<ComandoRemoverContato, bool>,
                                          IRequestHandler<ComandoAtualizarCliente, bool>,
                                          IRequestHandler<ComandoAtualizarContato, bool>
    {
        private readonly ICadastroQueries _cadastroQueries;
        private readonly IClienteRepository _clienteRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public CadastroCommandHandler(IClienteRepository clienteRepository,
                                      IMediatorHandler mediatorHandler,
                                      ICadastroQueries cadastroQueries)
        {
            _clienteRepository = clienteRepository;
            _mediatorHandler = mediatorHandler;
            _cadastroQueries = cadastroQueries;
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

            var contato = await _cadastroQueries.ObterContatoPorClienteId(comando.ClienteId);

            if (contato == null)
            {
                return false;
            }

            _clienteRepository.RemoverContato(contato.Id);

            return await _clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ComandoAtualizarCliente comando, CancellationToken cancellationToken)
        {
            if (!ValidarComando(comando))
            {
                return false;
            }

            var cliente = await _cadastroQueries.ObterCliente(comando.ClienteId);

            if(cliente == null)
            {
                return false;
            }

            cliente.AlterarNome(comando.Nome);
            cliente.AlterarSobrenome(comando.Sobrenome);
            cliente.AlterarCpf(comando.Cpf);
            cliente.AlterarSexo((Sexo)comando.Sexo);

            _clienteRepository.AtualizarCliente(cliente);

            return await _clienteRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ComandoAtualizarContato comando, CancellationToken cancellationToken)
        {
            if (!ValidarComando(comando))
            {
                return false;
            }

            var contato = await _cadastroQueries.ObterContatoPorClienteId(comando.ClienteId);

            if (contato == null)
            {
                return false;
            }

            contato.AlterarDdd(comando.Ddd);
            contato.AlterarTelefone(comando.Telefone);
            contato.AlterarEmail(comando.Email);

            _clienteRepository.AtualizarContato(contato);

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
