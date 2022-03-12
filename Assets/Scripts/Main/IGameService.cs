using System;
using System.Collections;

namespace Main
{
    public interface IGameService
    {
        /// <summary>
        /// Use Initialize to initialize your service. All services will have
        /// Initialize called before Setup.
        /// </summary>
        IEnumerator Initialize();

        /// <summary>
        /// Use Setup to get references to other Services through the IServiceProvider.
        /// </summary>
        /// <param name="services"></param>
        void Setup(IServiceProvider services);

        /// <summary>
        /// Shutdown will be called when the application is shutting down. Or the service is removed.
        /// </summary>
        void Shutdown();
    }
}