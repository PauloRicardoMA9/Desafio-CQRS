using System;
using MediatR;

namespace CQRS.Core.Messages
{
    public abstract class Evento : Mensagem, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Evento()
        {
            Timestamp = DateTime.Now;
        }
    }
}