using System;
using System.Collections.Generic;

namespace Product.UnitTest.Builders
{
    internal abstract class BaseBuilder<T> where T : class
    {
        protected T _instance;

        protected BaseBuilder() =>
            _instance = Activator.CreateInstance<T>();

        protected BaseBuilder(T instance) =>
            _instance = instance;

        public virtual BaseBuilder<T> Default() => this;

        public T Build() => _instance;

        public List<T> BuildList() => new() { _instance };

        public virtual List<T> BuildList(int length)
        {
            var result = new List<T>();

            for (int i = 0; i < length; i++)
                result.Add(Default().Build());

            return result;
        }
    }
}
