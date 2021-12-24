using System;

namespace ConsoleCreditCardProcessor.Tests.Builders
{
    public interface IBuilder<T> where T : new()
    {
        public void Reset();

        public T Get();
    }
}
