namespace Core.Services
{
    public interface IAudioService
    {
        bool AudioEnabled { get; }
        void SetAudioEnabled(object anchor, bool isEnabled);
    }
}