using System.Threading.Tasks;
using MediatR;
using CQRS.Core.Messages;
using CQRS.Core.Messages.CommonMessages.Notifications;

namespace CQRS.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> EnviarComando<T>(T comando) where T : Comando
        {
            return await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Evento
        {
            await _mediator.Publish(evento);
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : NotificacaoDeDominio
        {
            await _mediator.Publish(notificacao);
        }
    }
}