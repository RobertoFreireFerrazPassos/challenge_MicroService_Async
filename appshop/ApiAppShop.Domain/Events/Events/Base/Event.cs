using System;

namespace ApiAppShop.Domain.Events.Base
{
    public abstract class Event
    {
        private Guid Id { get; }
        private string Identifier { get; }
        private DateTime TimeStamp { get; }

        public Event(string identifier) {
            Id = new Guid();
            TimeStamp = new DateTime();
            Identifier = identifier;
        }
    }
}
