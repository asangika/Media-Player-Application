using MediaPlayer.Domain.src.MediaAggregate;
using MediaPlayer.Domain.src.UserAggregate;
using MediaPlayer.Service.src.Services;

namespace MediaPlayer.Controller.src
{
    public class PlayListController
    {
        private readonly PlayListService _playListService;

        public PlayListController(PlayListService playListService)
        {
            _playListService = playListService;
        }

        public PlayList CreatePlaylist(Account user, string playlistName)
        {
            if (user.Role != "User")
            {
                Console.WriteLine("Only users can create playlists.");
                return null;
            }

            return _playListService.CreatePlaylist(user, playlistName);
            Console.WriteLine($"Playlist '{playlistName}' created for user '{user.UserName}'.");
        }

        public void AddMediaToPlayList(PlayList playList, Media media)
        {
            _playListService.AddMediaToPlayList(playList, media);
        }

        public void RemoveMediaFromPlayList(PlayList playList, Media media)
        {
            _playListService.RemoveMediaFromPlayList(playList, media);
        }

        public void PlayMediaById(Account user, Guid playlistId, Guid mediaId)
        {
            if (user.Role != "User")
            {
                Console.WriteLine("Only users can play media.");
                return;
            }

            _playListService.PlayMediaById(playlistId, mediaId);
            Console.WriteLine($"Playing media '{mediaId}' in playlist '{playlistId}' for user '{user.UserName}'.");
        }

        public void PauseMedia(Account user, Guid playlistId, Guid mediaId)
        {
            if (user.Role != "User")
            {
                Console.WriteLine("Only users can pause media.");
                return;
            }

            _playListService.PauseMedia(playlistId, mediaId);
            Console.WriteLine($"Paused media '{mediaId}' in playlist '{playlistId}' for user '{user.UserName}'.");
        }

        public void StopMedia(Account user, Guid playlistId, Guid mediaId)
        {
            if (user.Role != "User")
            {
                Console.WriteLine("Only users can stop media.");
                return;
            }

            _playListService.StopMedia(playlistId, mediaId);
            Console.WriteLine($"Stopped media '{mediaId}' in playlist '{playlistId}' for user '{user.UserName}'.");
        }

        public void AdjustBrightness(PlayList playList, int brightness)
        {
            _playListService.AdjustBrightness(playList, brightness);
        }

        public void AdjustSoundEffect(PlayList playList, string soundEffect)
        {
            _playListService.AdjustSoundEffect(playList, soundEffect);
        }
    }
}