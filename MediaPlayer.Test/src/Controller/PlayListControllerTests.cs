using MediaPlayer.Domain.src.MediaAggregate;
using MediaPlayer.Domain.src.UserAggregate;
using Moq;
using MediaPlayer.Controller.src;
using MediaPlayer.Service.src.Services;

namespace MediaPlayer.Tests
{
    public class PlayListControllerTests
    {
        private readonly PlayListController _playListController;
        private readonly Mock<PlayListService> _mockPlayListService;

        public PlayListControllerTests()
        {
            _mockPlayListService = new Mock<PlayListService>();
            _playListController = new PlayListController(_mockPlayListService.Object);
        }

        [Fact]
        public void CreatePlaylist_ShouldReturnNewPlaylist()
        {
            // Arrange
            var user = new Account { AccountId = Guid.NewGuid(), UserName = "Test User" };
            string playlistName = "Test Playlist";
            var playlist = new PlayList { Name = playlistName, OwnerId = user.AccountId };

            _mockPlayListService
                .Setup(service => service.CreatePlaylist(user, playlistName));

            // Act
            var result = _playListController.CreatePlaylist(user, playlistName);

            // Assert
            _mockPlayListService.Verify(service => service.CreatePlaylist(user, playlistName), Times.Once);
        }


        [Fact]
        public void AddMedia_ShouldCallPlayListServiceAddMedia()
        {
            // Arrange
            var playList = new PlayList();
            var media = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio" };

            // Act
            _playListController.AddMediaToPlayList(playList, media);

            // Assert
            _mockPlayListService.Verify(x => x.AddMediaToPlayList(playList, media), Times.Once);
        }

        [Fact]
        public void RemoveMedia_ShouldCallPlayListServiceRemoveMedia()
        {
            // Arrange
            var playList = new PlayList();
            var media = new Audio { MediaId = Guid.NewGuid(), Name = "Test Audio" };

            // Act
            _playListController.RemoveMediaFromPlayList(playList, media);

            // Assert
            _mockPlayListService.Verify(x => x.RemoveMediaFromPlayList(playList, media), Times.Once);
        }

        [Fact]
        public void PlayMediaById_ShouldCallPlayListServicePlayMediaById()
        {
            // Arrange
            var playList = new PlayList();
            var mediaId = Guid.NewGuid();
            var user = new Account { Role = "User" };

            // Act
            _playListController.PlayMediaById(user, playList.Id, mediaId);

            // Assert
            _mockPlayListService.Verify(x => x.PlayMediaById(playList.Id, mediaId), Times.Once);
        }

        [Fact]
        public void Pause_ShouldCallPlayListServicePause()
        {
            // Arrange
            var playList = new PlayList();
            var mediaId = Guid.NewGuid();
            var user = new Account { Role = "User" };

            // Act
            _playListController.PauseMedia(user, playList.Id, mediaId);

            // Assert
            _mockPlayListService.Verify(x => x.PauseMedia(playList.Id, mediaId), Times.Once);
        }

        [Fact]
        public void Stop_ShouldCallPlayListServiceStop()
        {
            // Arrange
            var playList = new PlayList();
            var mediaId = Guid.NewGuid();
            var user = new Account { Role = "User" };

            // Act
            _playListController.StopMedia(user, playList.Id, mediaId);

            // Assert
            _mockPlayListService.Verify(x => x.StopMedia(playList.Id, mediaId), Times.Once);
        }

        [Fact]
        public void AdjustBrightness_ShouldCallPlayListServiceAdjustBrightness()
        {
            // Arrange
            var playList = new PlayList();
            int brightness = 70;

            // Act
            _playListController.AdjustBrightness(playList, brightness);

            // Assert
            _mockPlayListService.Verify(x => x.AdjustBrightness(playList, brightness), Times.Once);
        }

        [Fact]
        public void AdjustSoundEffect_ShouldCallPlayListServiceAdjustSoundEffect()
        {
            // Arrange
            var playList = new PlayList();
            string soundEffect = "BassBoost";

            // Act
            _playListController.AdjustSoundEffect(playList, soundEffect);

            // Assert
            _mockPlayListService.Verify(x => x.AdjustSoundEffect(playList, soundEffect), Times.Once);
        }
    }
}
