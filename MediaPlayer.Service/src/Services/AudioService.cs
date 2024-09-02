using MediaPlayer.Domain.src.MediaAggregate;

namespace MediaPlayer.Service.src.Services
{
    public class AudioService
    {
        private List<Audio> _audioCollection = new List<Audio>();
        public AudioService(List<Audio> audioCollection)
        {
            this._audioCollection = audioCollection;
        }

        public void AddMedia(Audio audio)
        {
            _audioCollection.Add(audio);
        }

        public Audio GetMediaByName(string name)
        {
            return _audioCollection.FirstOrDefault(a => a.Name == name);
        }

        public Audio GetMediaById(Guid id)
        {
            return _audioCollection.FirstOrDefault(a => a.MediaId == id);
        }

        public void RemoveMedia(Guid id)
        {
            var media = GetMediaById(id);
            if (media != null) _audioCollection.Remove(media);
        }

        public void UpdateMedia(Audio updatedAudio)
        {
            var media = GetMediaById(updatedAudio.MediaId);
            if (media != null)
            {
                media.Name = updatedAudio.Name;
                media.Duration = updatedAudio.Duration;
                media.SoundEffect = updatedAudio.SoundEffect;
                media.Volume = updatedAudio.Volume;
            }
        }

    }
}