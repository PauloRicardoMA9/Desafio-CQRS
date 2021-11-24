using System;
using FluentValidation.Results;
using MediatR;

namespace CQRS.Core.Messages
{
    public abstract class Comando : Mensagem, IRequest<bool>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ResultadoDaValidacao { get; set; }

        protected Comando()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}