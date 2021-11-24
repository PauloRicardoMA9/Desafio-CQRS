using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CQRS.Core.Messages.CommonMessages.Notifications
{
    public class NotificacaoDeDominioHandler : INotificationHandler<NotificacaoDeDominio>
    {
        private List<NotificacaoDeDominio> _notificacoes;

        public NotificacaoDeDominioHandler()
        {
            _notificacoes = new List<NotificacaoDeDominio>();
        }

        public Task Handle(NotificacaoDeDominio mensagem, CancellationToken cancellationToken)
        {
            _notificacoes.Add(mensagem);
            return Task.CompletedTask;
        }

        public virtual List<NotificacaoDeDominio> ObterNotificacoes()
        {
            return _notificacoes;
        }

        public virtual bool TemNotificacao()
        {
            return ObterNotificacoes().Any();
        }

        public void Dispose()
        {
            _notificacoes = new List<NotificacaoDeDominio>();
        }
    }
}