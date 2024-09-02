using MediaPlayer.Domain.src.MediaAggregate;
using MediaPlayer.Domain.src.UserAggregate;
using MediaPlayer.Service.src.Services;

namespace MediaPlayer.Controller.src
{
    public class AudioController
    {
        private readonly AudioService _audioService;
        public bool IsLoggedIn { get; private set; }
        public Account LoggedInAccount { get; private set; }


        public AudioController(AudioService audioService)
        {
            _audioService = audioService;
        }

        public void AddAudio(Audio audio)
        {
            if (IsLoggedIn && LoggedInAccount != null && LoggedInAccount.Role == "Admin")
            {
                _audioService.AddMedia(audio);
                Console.WriteLine($"Media '{audio.Name}' created successfully.");
            }
            else
            {
                Console.WriteLine("Only admins can add new media.");
            }
        }
        public Audio GetMediaById(Guid id)
        {

            return _audioService.GetMediaById(id);

        }

        public void RemoveAudio(Guid id)
        {
            if (IsLoggedIn && LoggedInAccount != null && LoggedInAccount.Role == "Admin")
            {
                _audioService.RemoveMedia(id);
                Console.WriteLine($"Audio with id  '{id}' removed successfully.");
            }
            else
            {
                Console.WriteLine("Only admins can remove media.");
            }
        }

        public void UpdateAudio(Audio audio)
        {
            if (IsLoggedIn && LoggedInAccount != null && LoggedInAccount.Role == "Admin")
            {
                _audioService.UpdateMedia(audio);
                Console.WriteLine($"User '{audio.Name}' updated successfully.");
            }
            else
            {
                Console.WriteLine("Only admins can update media.");
            }
        }

    }
}