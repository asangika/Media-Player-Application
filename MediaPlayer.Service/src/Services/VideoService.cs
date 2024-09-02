using MediaPlayer.Domain.src.MediaAggregate;

namespace MediaPlayer.Service.src.Services
{
    public class VideoService
    {
        private List<Video> _videoCollection = new List<Video>();

        public VideoService(List<Video> videoCollection)
        {
            this._videoCollection = videoCollection;
        }
        public void AddMedia(Video video)
        {
            _videoCollection.Add(video);
        }

        public Video GetMediaByName(string name)
        {
            return _videoCollection.FirstOrDefault(a => a.Name == name);
        }

        public Video GetMediaById(Guid id)
        {
            return _videoCollection.FirstOrDefault(v => v.MediaId == id);
        }

        public void RemoveMedia(Guid id)
        {
            var media = GetMediaById(id);
            if (media != null) _videoCollection.Remove(media);
        }

        public void UpdateMedia(Video updatedVideo)
        {
            var media = GetMediaById(updatedVideo.MediaId);
            if (media != null)
            {
                media.Name = updatedVideo.Name;
                media.Duration = updatedVideo.Duration;
                media.Brightness = updatedVideo.Brightness;
                media.Volume = updatedVideo.Volume;
            }
        }

    }
}
