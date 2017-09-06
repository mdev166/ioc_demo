using System;

namespace IoC
{
    /// <summary>
    /// Class to contain IoC registered types
    /// </summary>
    public class Registration
    {
        public Type AbstractType { get; set; }
        public Type ConcreteType { get; set; }
        public LifeCycleType LifeCycleType { get; set; }
        public object Instance { get; set; }
    }
}
