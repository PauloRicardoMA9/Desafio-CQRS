using CQRS.Core.DomainObjects;
using System;

namespace CQRS.Core.Data
{
    //Um repositório por agregação
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
