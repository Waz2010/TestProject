using System;
namespace JsonToJsonCompareEngine
{
    public static class Factory
    {
        public static TData Create<TData>()
        {
            return Activator.CreateInstance<TData>();
        }

        public static TData Create<TData>(object[] args)
        {
            return (TData) Activator.CreateInstance(typeof(TData), args:args);
        }
    }
}
