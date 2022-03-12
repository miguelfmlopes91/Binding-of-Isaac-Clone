using System;
using System.Collections;
using Core.Services;

namespace Core.Audio
{
    public class AudioService : IAudioService, IGameService
    {
        private IServiceProvider _services;
        public bool AudioEnabled { get; }
        public void SetAudioEnabled(object anchor, bool isEnabled)
        {
            throw new NotImplementedException();
        }

        public IEnumerator Initialize()
        {
            yield return null;
        }

        public void Setup(IServiceProvider services)
        {
            _services = services;
            //XService = services.GetService<IXService>();
        }

        public void Shutdown()
        {
            
        }
    }
}