using System.Threading.Tasks;
using CQRS.Core.Messages;
using CQRS.Core.Messages.CommonMessages.Notifications;

namespace CQRS.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Evento;
        Task<bool> EnviarComando<T>(T comando) where T : Comando;
        Task PublicarNotificacao<T>(T notificacao) where T : NotificacaoDeDominio;
    }
}