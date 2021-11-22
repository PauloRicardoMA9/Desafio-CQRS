using System;

namespace CQRS.Core.DomainObjects
{
    public abstract class Entidade
    {
        public Guid Id { get; set; }

        protected Entidade()
        {
            Id = new Guid();
        }
    }
}
