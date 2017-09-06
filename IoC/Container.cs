using System;
using System.Collections.Generic;
using System.Linq;

namespace IoC
{
    /// <summary>
    /// Inversion of control container
    /// </summary>
    public class Container
    {
        private readonly IList<Registration> registrations = new List<Registration>();

        /// <summary>
        /// Register type
        /// </summary>
        /// <typeparam name="TAbstractType"></typeparam>
        /// <typeparam name="TConcreteType"></typeparam>
        /// <param name="lifeCycleType"></param>
        public void Register<TAbstractType, TConcreteType>(LifeCycleType lifeCycleType = LifeCycleType.Transient)
        {
            var typeString = typeof(TAbstractType).ToString();
            try
            {
                // remove registration
                if (registrations.FirstOrDefault(r => r.AbstractType == typeof(TAbstractType)) != null)
                    UnRegister<TAbstractType>();

                // register type
                registrations.Add(new Registration
                {
                    AbstractType = typeof(TAbstractType),
                    ConcreteType = typeof(TConcreteType),
                    LifeCycleType = lifeCycleType
                });
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("There was an error registering '{0}'.", typeString), ex);
            }
        }

        /// <summary>
        /// Un-register type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void UnRegister<T>()
        {
            var typeString = typeof(T).ToString();
            try
            {
                registrations.Remove(registrations.FirstOrDefault(r => r.AbstractType == typeof(T)));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("There was an error un-registering '{0}'.", typeString), ex);
            }
        }

        /// <summary>
        /// Resolve type, create or retreive concrete instance
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            var typeString = type.ToString();
            try
            {
                var registration = registrations.FirstOrDefault(t => t.AbstractType == type);
                if (registration == null)
                    throw new TypeNotRegisteredException(String.Format("Type '{0}' not registered with Container instance.", typeString));

                return CreateInstance(registration);
            }
            catch (TypeNotRegisteredException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error resolving '{0}'.", typeString), ex);
            }
        }

        /// <summary>
        /// Supply instance from registration type
        ///     LifeCycleType.Transient - create a new instance
        ///     LifeCycleType.Singleton - re-use instance
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        private object CreateInstance(Registration registration)
        {
            var parameters = new List<object>();
            object instance = null;

            var constructor = registration.ConcreteType.GetConstructors().FirstOrDefault();
            if (constructor != null)
            {
                foreach (var param in constructor.GetParameters())
                {
                    var parmType = registrations.FirstOrDefault(r => r.AbstractType == param.ParameterType);
                    if (parmType == null)
                        throw new TypeNotRegisteredException(String.Format("Type '{0}' not registered with Container instance.", param.ParameterType.ToString()));

                    parameters.Add(CreateInstance(parmType));
                }
            }

            if (registration.LifeCycleType == LifeCycleType.Transient)
            {
                // create new instance
                instance = Activator.CreateInstance(registration.ConcreteType, parameters.ToArray());
            }
            else
            {
                if (registration.Instance == null)
                    registration.Instance = Activator.CreateInstance(registration.ConcreteType, parameters.ToArray());

                // return instance
                instance = registration.Instance;
            }

            return instance;
        }
    }

    #region custom exception

    /// <summary>
    /// Custom exception for un-registered types
    /// </summary>
    public class TypeNotRegisteredException : ApplicationException
    {
        public TypeNotRegisteredException(string message) : base(message) { }
    }

    #endregion custom exception
}