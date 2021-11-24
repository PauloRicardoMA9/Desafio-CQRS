using CQRS.Core.Communication.Mediator;
using CQRS.Core.Messages;
using CQRS.Core.Messages.CommonMessages.Notifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Cadastro.Application.Commands
{
    public class CadastroCommandHandler : IRequestHandler<ComandoAdicionarCliente, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public async Task<bool> Handle(ComandoAdicionarCliente comando, CancellationToken cancellationToken)
        {
            if (!ValidarComando(comando))
            {
                return false;
            }

            return true;
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
