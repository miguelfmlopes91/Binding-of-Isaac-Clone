using System;
using System.Collections.Generic;

namespace Core.Services
{
    public class Services : IServiceProvider
    {
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public void AddService(Type serviceType, object serviceInstance)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            if (serviceInstance == null)
            {
                throw new ArgumentNullException("serviceInstance");
            }
            if (!serviceType.IsInstanceOfType(serviceInstance))
            {
                throw new ArgumentException("AddService must be called with an instance of specified type.", "serviceInstance");
            }

            services.Add(serviceType, serviceInstance);
        }
        
        public IEnumerable<object> GetServices()
        {
            foreach (object service in services.Values)
            {
                yield return service;
            }
        }
        
        public bool RemoveService(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            return services.Remove(serviceType);
        }

        public T GetService<T>() where T : class
        {
            return GetService(typeof(T)) as T;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            services.TryGetValue(serviceType, out object value);

            return value;
        }
        
    }
}