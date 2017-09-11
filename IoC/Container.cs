using System;
using System.Collections.Generic;
using System.Linq;

namespace IoC
{
    /// <summary>
    /// Inversion of control container
    /// </summary>
    public class Container : IContainer
    {
        private readonly Dictionary<string, Registration> registrations = new Dictionary<string, Registration>();

        /// <summary>
        /// Register type
        /// </summary>
        /// <typeparam name="TAbstractType"></typeparam>
        /// <typeparam name="TConcreteType"></typeparam>
        /// <param name="lifeCycleType"></param>
        public void Register<TAbstractType, TConcreteType>(LifeCycleType lifeCycleType = LifeCycleType.Transient)
        {
            var typeName = typeof(TAbstractType).ToString();
            try
            {
                // remove registration
                if (registrations.ContainsKey(typeName))
                    UnRegister<TAbstractType>();

                // register
                registrations.Add(typeName, new Registration
                {
                    AbstractType = typeof(TAbstractType),
                    ConcreteType = typeof(TConcreteType),
                    LifeCycleType = lifeCycleType
                });
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("There was an error registering '{0}'.", typeName), ex);
            }
        }

        /// <summary>
        /// Resolve type, create or retrieve concrete instance
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            var typeName = type.ToString();
            try
            {
                return GetInstance(GetRegistration(typeName));
            }
            catch (TypeNotRegisteredException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error resolving '{0}'.", typeName), ex);
            }
        }

        /// <summary>
        /// Un-register type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void UnRegister<T>()
        {
            var typeName = typeof(T).ToString();
            try
            {
                registrations.Remove(typeName);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("There was an error un-registering '{0}'.", typeName), ex);
            }
        }

        /// <summary>
        /// Supply instance from registration type
        ///     LifeCycleType.Transient - create a new instance
        ///     LifeCycleType.Singleton - re-use instance
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        private object GetInstance(Registration registration)
        {
            var parameters = new List<object>();
            object instance = null;

            var constructor = registration.ConcreteType.GetConstructors().FirstOrDefault();
            if (constructor != null)
            {
                foreach (var param in constructor.GetParameters())
                {
                    parameters.Add(GetInstance(GetRegistration(param.ParameterType.ToString())));
                }
            }

            if (registration.LifeCycleType == LifeCycleType.Transient)
            {
                // create new instance
                instance = Activator.CreateInstance(registration.ConcreteType, parameters.ToArray());
            }
            else
            {
                // create new instance if one does not exist
                if (registration.Instance == null)
                    registration.Instance = Activator.CreateInstance(registration.ConcreteType, parameters.ToArray());

                // return instance
                instance = registration.Instance;
            }

            return instance;
        }

        /// <summary>
        /// Retrieve a registration, throw an exception if not found
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private Registration GetRegistration(string typeName)
        {
            Registration registration;
            var found = registrations.TryGetValue(typeName, out registration);
            if (!found)
                throw new TypeNotRegisteredException(String.Format("Type '{0}' not registered with Container instance.", typeName));

            return registration;
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