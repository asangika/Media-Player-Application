using MediaPlayer.Domain.src.MediaAggregate;
using MediaPlayer.Domain.src.UserAggregate;

namespace MediaPlayer.Service.src.Services
{
    public class PlayListService
    {
        private readonly List<PlayList> _playlists = new List<PlayList>();

        public virtual PlayList CreatePlaylist(Account user, string playlistName)
        {
            if (string.IsNullOrWhiteSpace(playlistName))
                throw new ArgumentException("Playlist name cannot be empty.", nameof(playlistName));

            var playlist = new PlayList
            {
                Id = Guid.NewGuid(),
                Name = playlistName,
                OwnerId = user.AccountId
            };
            user.PlayLists.Add(playlist);
            _playlists.Add(playlist);
            return playlist;
        }

        public virtual void AddMediaToPlayList(PlayList playList, Media media)
        {
            if (media != null && !playList.Items.Contains(media))
            {
                playList.Items.Add(media);
                Console.WriteLine($"{media.Name} added to playlist.");
            }
            else
            {
                Console.WriteLine("Media is null or already exists in the playlist.");
            }
        }

        public virtual void RemoveMediaFromPlayList(PlayList playList, Media media)
        {
            if (media != null && playList.Items.Contains(media))
            {
                playList.Items.Remove(media);
                Console.WriteLine($"{media.Name} removed from playlist.");
            }
            else
            {
                Console.WriteLine("Media is null or does not exist in the playlist.");
            }
        }

        public virtual void PlayMediaById(Guid playlistId, Guid mediaId)
        {
            var playlist = _playlists.FirstOrDefault(p => p.Id == playlistId);
            if (playlist != null)
            {
                var media = playlist.Items.FirstOrDefault(m => m.MediaId == mediaId);
                if (media != null)
                {
                    media.IsPlaying = true;
                    media.IsPaused = false;
                    media.IsStopped = false;
                    playlist.PlayingItem = media;
                    Console.WriteLine($"Playing {media.Name} of the PlayList {playlist.Name}");
                }
                else
                {
                    Console.WriteLine("Media not found in the playlist.");
                }
            }
        }

        public virtual void PauseMedia(Guid playlistId, Guid mediaId)
        {
            var playlist = _playlists.FirstOrDefault(p => p.Id == playlistId);
            if (playlist != null)
            {
                var media = playlist.Items.FirstOrDefault(m => m.MediaId == mediaId);
                if (media != null)
                {
                    media.IsPlaying = false;
                    media.IsPaused = true;
                    media.IsStopped = false;
                    Console.WriteLine($"Paused {media.Name} of the PlayList {playlist.Name}");
                }
                else
                {
                    Console.WriteLine("Media not found in the playlist.");
                }
            }
        }

        public virtual void StopMedia(Guid playlistId, Guid mediaId)
        {
            var playlist = _playlists.FirstOrDefault(p => p.Id == playlistId);
            if (playlist != null)
            {
                var media = playlist.Items.FirstOrDefault(m => m.MediaId == mediaId);
                if (media != null)
                {
                    media.IsPlaying = false;
                    media.IsPaused = false;
                    media.IsStopped = true;
                    Console.WriteLine($"Stopped {media.Name} of the PlayList {playlist.Name}");
                }
                else
                {
                    Console.WriteLine("Media not found in the playlist.");
                }
            }
        }

        public virtual void AdjustBrightness(PlayList playList, int brightness)
        {
            if (playList.PlayingItem is Video video)
            {
                Console.WriteLine($"Brightness set to {brightness} for {video.Name}");
                video.Brightness = brightness;
            }
            else
            {
                Console.WriteLine("Cannot adjust brightness unless a video is playing.");
            }
        }

        public virtual void AdjustSoundEffect(PlayList playList, string soundEffect)
        {
            if (playList.PlayingItem is Audio audio)
            {
                Console.WriteLine($"Sound effect set to {soundEffect} for {audio.Name}");
                audio.SoundEffect = soundEffect;
            }
            else
            {
                Console.WriteLine("Cannot adjust sound effect unless an audio is playing.");
            }
        }
    }
}