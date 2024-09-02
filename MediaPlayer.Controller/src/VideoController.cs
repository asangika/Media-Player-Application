using MediaPlayer.Domain.src.MediaAggregate;
using MediaPlayer.Domain.src.UserAggregate;
using MediaPlayer.Service.src.Services;

namespace MediaPlayer.Controller.src
{
    public class VideoController
    {
        private readonly VideoService _videoService;
        public bool IsLoggedIn { get; private set; }
        public Account LoggedInAccount { get; private set; }


        public VideoController(VideoService videoService)
        {
            _videoService = videoService;
        }

        public void AddVideo(Video video)
        {
            _videoService.AddMedia(video);
        }
        public Video GetMediaById(Guid id)
        {
            return _videoService.GetMediaById(id);
        }

        public void RemoveVideo(Guid id)
        {
            if (IsLoggedIn && LoggedInAccount != null && LoggedInAccount.Role == "Admin")
            {
                _videoService.RemoveMedia(id);
                Console.WriteLine($"Video with id '{id}' removed successfully.");
            }
            else
            {
                Console.WriteLine("Only admins can remove media.");
            }
        }

        public void UpdateVideo(Video video)
        {
            if (IsLoggedIn && LoggedInAccount != null && LoggedInAccount.Role == "Admin")
            {
                _videoService.UpdateMedia(video);
                Console.WriteLine($"User '{video.Name}' updated successfully.");
            }
            else
            {
                Console.WriteLine("Only admins can update media.");
            }
        }
    }
}