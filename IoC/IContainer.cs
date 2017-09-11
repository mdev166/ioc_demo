using System;

namespace IoC
{
    public interface IContainer
    {
        void Register<TAbstractType, TConcreteType>(LifeCycleType lifeCycleType = LifeCycleType.Transient);
        object Resolve(Type type);
        void UnRegister<T>();
    }
}
