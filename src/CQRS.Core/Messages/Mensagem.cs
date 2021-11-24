using System;

namespace CQRS.Core.Messages
{
    public abstract class Mensagem
    {
        public string TipoDaMensagem { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Mensagem()
        {
            TipoDaMensagem = GetType().Name;
        }
    }
}