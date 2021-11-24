using System;
using MediatR;

namespace CQRS.Core.Messages.CommonMessages.Notifications
{
    public class NotificacaoDeDominio : Mensagem, INotification
    {
        public DateTime Timestamp { get; private set; }
        public Guid NotificacaoDeDominioId { get; private set; }
        public string Key { get; private set; }
        public string Valor { get; private set; }
        public int Versao { get; private set; }

        public NotificacaoDeDominio(string key, string valor)
        {
            Timestamp = DateTime.Now;
            NotificacaoDeDominioId = Guid.NewGuid();
            Versao = 1;
            Key = key;
            Valor = valor;
        }
    }
}