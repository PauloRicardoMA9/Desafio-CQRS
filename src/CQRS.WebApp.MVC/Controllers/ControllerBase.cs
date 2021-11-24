using System.Collections.Generic;
using System.Linq;
using CQRS.Core.Communication.Mediator;
using CQRS.Core.Messages.CommonMessages.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.WebApp.MVC.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private readonly NotificacaoDeDominioHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;

        protected ControllerBase(INotificationHandler<NotificacaoDeDominio> notificacoes, 
                                 IMediatorHandler mediatorHandler)
        {
            _notifications = (NotificacaoDeDominioHandler)notificacoes;
            _mediatorHandler = mediatorHandler;
        }

        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }

        protected IEnumerable<string> ObterMensagensErro()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Valor).ToList();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new NotificacaoDeDominio(codigo, mensagem));
        }
    }
}