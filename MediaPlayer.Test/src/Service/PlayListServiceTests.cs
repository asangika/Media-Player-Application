using MediaPlayer.Domain.src.MediaAggregate;
using MediaPlayer.Domain.src.UserAggregate;
using MediaPlayer.Service.src.Services;

namespace MediaPlayer.Test.src.Service
{
    public class PlayListServiceTests
    {
        private readonly PlayListService _playListService;

        public PlayListServiceTests()
        {
            _playListService = new PlayListService();
        }

        [Fact]
        public void CreatePlaylist_ShouldReturnNewPlaylist()
        {
            // Arrange
            var user = new Account { AccountId = Guid.NewGuid(), UserName = "Test User", PlayLists = new List<PlayList>() };
            string playlistName = "Test Playlist";

            // Act
            var result = _playListService.CreatePlaylist(user, playlistName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(playlistName, result.Name);
            Assert.Equal(user.AccountId, result.OwnerId);
            Assert.Contains(result, user.PlayLists);
        }

        [Fact]
        public void AddMedia_ShouldAddMediaToPlayList()
        {

            var playlist = new PlayList();
            var media = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio" };

            _playListService.AddMediaToPlayList(playlist, media);

            Assert.Contains(media, playlist.Items);
        }
        [Fact]
        public void CreatePlaylist_ShouldThrowException_WhenPlaylistNameIsEmpty()
        {
            // Arrange
            var user = new Account { AccountId = Guid.NewGuid(), UserName = "Test User", PlayLists = new List<PlayList>() };

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _playListService.CreatePlaylist(user, ""));
            Assert.Equal("Playlist name cannot be empty. (Parameter 'playlistName')", exception.Message);
        }

        [Fact]
        public void RemoveMedia_ShouldRemoveMediaFromPlayList()
        {
            var playlist = new PlayList();
            var media = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio" };
            playlist.Items.Add(media);


            _playListService.RemoveMediaFromPlayList(playlist, media);

            Assert.DoesNotContain(media, playlist.Items);
        }

        [Fact]
        public void CreatePlaylist_ShouldAddPlaylistToUserPlaylists()
        {
            // Arrange
            var user = new Account { AccountId = Guid.NewGuid(), UserName = "Test User", PlayLists = new List<PlayList>() };
            string playlistName = "Test Playlist";

            // Act
            var result = _playListService.CreatePlaylist(user, playlistName);

            // Assert
            Assert.Contains(result, user.PlayLists);
        }

        [Fact]
        public void PlayMediaById_ShouldSetPlayingItem()
        {
            // Arrange
            var playlist = new PlayList();
            var media = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio" };
            playlist.Items.Add(media);

            // Act
            _playListService.PlayMediaById(playlist.Id, media.MediaId);

            // Assert
            Assert.Equal(media, playlist.PlayingItem);
        }

        [Fact]
        public void Pause_ShouldOutputPausedMessage()
        {
            // Arrange
            var playlist = new PlayList();
            var media = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio" };
            playlist.Items.Add(media);
            _playListService.PlayMediaById(playlist.Id, media.MediaId);

            // Act & Assert
            var ex = Record.Exception(() => _playListService.PauseMedia(playlist.Id, media.MediaId));
            Assert.Null(ex);
        }

        [Fact]
        public void Stop_ShouldClearPlayingItem()
        {
            // Arrange
            var playlist = new PlayList();
            var media = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio" };
            playlist.Items.Add(media);
            _playListService.PlayMediaById(playlist.Id, media.MediaId);

            // Act
            _playListService.StopMedia(playlist.Id, media.MediaId);

            // Assert
            Assert.Null(playlist.PlayingItem);
        }

        [Fact]
        public void AdjustBrightness_ShouldSetBrightnessForVideo()
        {
            // Arrange
            var playlist = new PlayList();
            var video = new Video { MediaId = Guid.NewGuid(), Name = "Test Video", Brightness = 50 };
            playlist.Items.Add(video);
            _playListService.PlayMediaById(playlist.Id, video.MediaId);

            // Act
            _playListService.AdjustBrightness(playlist, 70);

            // Assert
            Assert.Equal(70, video.Brightness);
        }

        [Fact]
        public void AdjustSoundEffect_ShouldSetSoundEffectForAudio()
        {
            // Arrange
            var playlist = new PlayList();
            var audio = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio", SoundEffect = "None" };
            playlist.Items.Add(audio);
            _playListService.PlayMediaById(playlist.Id, audio.MediaId);

            // Act
            _playListService.AdjustSoundEffect(playlist, "BassBoost");

            // Assert
            Assert.Equal("BassBoost", audio.SoundEffect);
        }

    }

}