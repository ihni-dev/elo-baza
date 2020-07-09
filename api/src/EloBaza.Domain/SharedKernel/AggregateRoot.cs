using MediatR;
using System;
using System.Collections.Generic;

namespace EloBaza.Domain.SharedKernel
{
    public abstract class AggregateRoot : Entity 
    {
        private readonly List<INotification> _domainEvents = new List<INotification>();
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(INotification newEvent)
        {
            _domainEvents.Add(newEvent);
        }

        public void ClearEvents()
        {
            _domainEvents.Clear();
        }
    }
}
