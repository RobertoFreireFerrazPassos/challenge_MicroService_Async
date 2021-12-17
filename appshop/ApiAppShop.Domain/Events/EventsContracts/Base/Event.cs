using System;

namespace ApiAppShop.Domain.Events
{
    public abstract class Event
    {
        private Guid Id { get; }
        private string Identifier { get; }
        private DateTime TimeStamp { get; }

        public Event(string identifier) {
            Id = Guid.NewGuid();
            TimeStamp = DateTime.UtcNow;
            Identifier = identifier;
        }
    }
}
